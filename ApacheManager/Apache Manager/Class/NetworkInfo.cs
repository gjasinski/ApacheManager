using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Apache_Manager.Class
{
    class NetworkInfo
    {
        public List<string> messages = new List<string>();
        public bool testPort(IPAddress address, int port)
        {
            try
            {
                TcpClient client = new TcpClient(address.ToString(), port);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd : " + ex.Message, "Błąd");
                return false;
            }
        }
        public void PingAsynch(string address, int timeout, byte[] buffer, PingOptions options)
        {
            Ping ping = new Ping();
            ping.PingCompleted += new PingCompletedEventHandler(EndPing);
            try
            {
                ping.SendAsync(address, timeout, buffer, options, null);
            }
            catch (Exception ex)
            {
                this.messages.Add("Błąd " + address + " : " + ex.Message);
            }
        }
        public void EndPing(object sender, PingCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                this.messages.Add("Błąd : Operacja przerwana bądź nieprawidłowy adres");
                ((IDisposable)(Ping)sender).Dispose();
                return;
            }
            PingReply reply = e.Reply;
            if (reply.Status == IPStatus.Success)
            {
                this.messages.Add("Odpowiedz z " + reply.Address.ToString() + " bajtow " + reply.Buffer.Length + " czas " + reply.RoundtripTime + " TTL " + reply.Options.Ttl);
            }
            else
            {
                this.messages.Add("Błąd " + e.Reply.Address + " " + reply.Status);
            }
            ((IDisposable)(Ping)sender).Dispose();
        }
        public List<string> cardInfo(int id)
        {
            List<string> list = new List<string>();

            NetworkInterface[] nic = NetworkInterface.GetAllNetworkInterfaces(); 
            IPGlobalProperties ipp = IPGlobalProperties.GetIPGlobalProperties();
            list.Add("Host name : " + ipp.HostName);
            list.Add("Domain name : " + ipp.DomainName);
            list.Add("Card " +id + " " + nic[id].Id);
            //list.Add("public IP : " + IPAddress);
            list.Add("private IP : " );
            list.Add(nic[id].GetPhysicalAddress().ToString());
            list.Add("DNS");
            foreach (IPAddress ip in nic[id].GetIPProperties().DnsAddresses) Console.WriteLine(ip);
            list.Add("Gateway");
            foreach (GatewayIPAddressInformation ip in nic[id].GetIPProperties().GatewayAddresses) Console.WriteLine(ip.Address.ToString());
            list.Add(nic[id].Speed / 1000000 + "Mb/s");
            list.Add("");
            return list;
        }
    }
}
