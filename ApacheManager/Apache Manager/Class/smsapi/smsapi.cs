using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace Apache_Manager.Class.smsapi
{
    class smsapi
    {
        public void send()
        {
            try
            {
                SMSApi.Api.Client client = new SMSApi.Api.Client("lewinski.marek@outlook.com");
                client.SetPasswordHash("85b40f9bde5fbde0343205248b4cba31");

                var smsApi = new SMSApi.Api.SMSFactory(client);

                var result =
                    smsApi.ActionSend()
                        .SetText("Chyba działa")
                        .SetTo("508796647")
                        .SetSender("ECO") //Pole nadawcy lub typ wiadomość 'ECO', '2Way'
                        .Execute();
            }
            catch (SMSApi.Api.ActionException e)
            {
                /**
                 * Błędy związane z akcją (z wyłączeniem błędów 101,102,103,105,110,1000,1001 i 8,666,999,201)
                 * http://www.smsapi.pl/sms-api/kody-bledow
                 */
                System.Console.WriteLine(e.Message);
            }
            catch (SMSApi.Api.ClientException e)
            {
                /**
                 * 101 Niepoprawne lub brak danych autoryzacji.
                 * 102 Nieprawidłowy login lub hasło
                 * 103 Brak punków dla tego użytkownika
                 * 105 Błędny adres IP
                 * 110 Usługa nie jest dostępna na danym koncie
                 * 1000 Akcja dostępna tylko dla użytkownika głównego
                 * 1001 Nieprawidłowa akcja
                 */
                System.Console.WriteLine(e.Message);
            }
            catch (SMSApi.Api.HostException e)
            {
                /* błąd po stronie servera lub problem z parsowaniem danych
                 * 
                 * 8 - Błąd w odwołaniu
                 * 666 - Wewnętrzny błąd systemu
                 * 999 - Wewnętrzny błąd systemu
                 * 201 - Wewnętrzny błąd systemu
                 * SMSApi.Api.HostException.E_JSON_DECODE - problem z parsowaniem danych
                 */
                System.Console.WriteLine(e.Message);
            }
            catch (SMSApi.Api.ProxyException e)
            {
                // błąd w komunikacji pomiedzy klientem a serverem
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
