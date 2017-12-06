using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScotTarg.SingleTarget
{
    public class TargetComms
    {
        private TargetConnection _connection = new TargetConnection(1);
        private System.Timers.Timer pollTimer;
        private bool asyncBusy = false;

        public event EventHandler<ShotRecordedEventArgs> OnHitRecorded;

        public TargetComms()
        {
            pollTimer = new System.Timers.Timer(1000);
            pollTimer.Elapsed += PollTimer_Elapsed;
        }

        public void Disconnect()
        {
            if (_connection.Connected)
            {
                pollTimer.Stop();
                _connection.Disconnect();
            }
        }

        public void Connect(string hostName)
        {
            if (!_connection.Connected)
            {
                if (_connection.Connect(hostName))
                {
                    pollTimer.Start();
                }
            }
        }

        private void PollTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            TargetCommand cmd = new TargetCommand();
            cmd.CommandByte = CommandByte.GET_LAST_SHOT;
            cmd.Data = new byte[0];
            SendCommandAsynch(cmd);
        }

        /// <summary>
        /// Start an asynchronous process that sends a command to the target and waits for a reply.
        /// When the reply is received, it is processed and if appropriate, relavent event is raised;
        /// </summary>
        /// <param name="cmd"></param>
        private void SendCommandAsynch(ScotTarg.TargetTools.TargetCommand cmd)
        {
            while (asyncBusy)
            {
                Thread.Sleep(10);
            }
            Thread thread = new Thread(GetTargetDataWorker);
            thread.Start(cmd);
        }

        /// <summary>
        /// Send command to the target and wait for the reply
        /// </summary>
        /// <param name="args"></param>
        private void GetTargetDataWorker(object args)
        {
            asyncBusy = true;
            TargetCommand cmd = (TargetCommand)args;
            _connection.SendTargetCommand(cmd);
            byte[] replyPacket = _connection.GetTargetReply();
            asyncBusy = false;
            if (replyPacket.Length > 0)
            {
                try
                {
                    TargetCommand reply = TargetCommand.Parse(replyPacket);
                    ProcessCommand(reply);
                }
                catch { }
            }
        }

        public void ProcessCommand(TargetCommand msg)
        {
            switch (msg.CommandByte)
            {
                case CommandByte.GET_LAST_SHOT:
                case CommandByte.GET_SHOT:
                    processShotData(msg.Data);
                    break;
            }

        }

        private void processShotData(byte[] data)
        {
            if (data.Length >= 9)
            {
                uint id = (uint)(data[0] << 8 | data[1]);
                int t1 = data[2] << 8 | data[3];
                int t2 = data[4] << 8 | data[5];
                int t3 = data[6] << 8 | data[7];
                int t4 = data[8] << 8 | data[9];
                ShotData shot = new ShotData(1, id, DateTime.Now, t1, t2, t3, t4);
                if (OnHitRecorded != null)
                {
                    RaiseEventOnUIThread(OnHitRecorded, new ShotRecordedEventArgs(shot));
                }
            }
        }

        /// <summary>
        /// Raise an event on a different thread than the current one
        /// </summary>
        private void RaiseEventOnUIThread(Delegate theEvent, object args)
        {
            try
            {
                foreach (Delegate d in theEvent.GetInvocationList())
                {
                    ISynchronizeInvoke syncer = d.Target as ISynchronizeInvoke;
                    if (syncer == null)
                    {
                        d.DynamicInvoke(args);
                    }
                    else
                    {
                        syncer.Invoke(d, new[] { this, args });  // cleanup omitted    
                    }
                    //_thread.Join();
                }
            }
            catch //(Exception ex)
            {
                //Exception code
            }
        }
    }
}
