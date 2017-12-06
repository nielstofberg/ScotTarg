using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScotTargCalculationTest
{
    public class Comms
    {
        TargetConnection connection = new TargetConnection(1);

        public event EventHandler<PacketReceivedEventArgs> OnMessageReceived;

        public Comms()
        {
        }

        public void Connect(string portName)
        {
            connection.Connect(portName);
        }


        public void Disconnect()
        {
            connection.Disconnect();
        }

        public void PollTarget()
        {
            TargetCommand cmd = new TargetCommand();
            cmd.CommandByte = CommandByte.GET_LAST_SHOT;
            cmd.Data = new byte[0];
            SendCommandAsynch(cmd);
        }

        bool asyncBusy = false;
        public void SendCommandAsynch(TargetCommand cmd)
        {
            while (!asyncBusy)
            {
                Thread.Sleep(10);
            }
            Thread thread = new Thread(GetTargetDataWorker);
            thread.Start(cmd);
        }

        private void GetTargetDataWorker(object args)
        {
            asyncBusy = true;
            TargetCommand cmd = (TargetCommand)args;
            connection.SendTargetCommand(cmd);
            byte[] reply = connection.GetTargetReply();
            asyncBusy = false;
            if (reply.Length>0 && OnMessageReceived != null)
            {
                
                RaiseEventOnUIThread(OnMessageReceived, new PacketReceivedEventArgs(TargetCommand.Parse(reply)));
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
