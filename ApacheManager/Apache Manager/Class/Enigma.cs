using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Apache_Manager.Class
{
    class Enigma
    {
        /// <summary>
        /// Klasa Enigma
        /// Lista rotors zawiera układ znaków w wirnikach
        /// Lista shofts zawiera numer znaku przy którm jest przeniesienie obrotu
        /// </summary>
        private List<string>rotors = new List<string>();
        private List<int> shifts = new List<int>();
        private void initRotors()
        {
            ///<summary>
            ///Lista rotors(wirniki):
            ///index    -   opis
            ///0    -       ORYGINALNY ZESTAW ZNAKOW
            ///1    -       PIERWSZY ZESTAW ZNAKOW
            ///2    -       DRUGI ZESTAW ZNAKOW
            ///3    -       TRZECI ZESTAW ZNAKOW
            ///4    -       CZWARTY ZESTAW ZNAKOW
            ///5    -       PIATY ZESTAW ZNAKOW
            ///6    -       SZÓSTY ZESTAW ZNAKOW
            ///7    -       SIÓDMY ZESTAW ZNAKOW
            ///8    -       ÓSMY ZESTAW ZNAKOW
            ///9    -       ZESTAW ZNAKOW BETA
            ///10   -       ZESTAW ZNAKOW GAMMA
            ///11   -       BĘBEN ODWRACAJACY B - INDEX 11
            ///12   -       BĘBEN ODWRACAJACY C - INDEX 12
            ///13   -       reflektor B Dünn (BĘBEN ODWRACAJACY) - INDEX 13
            ///14   -       reflektor C Dünn (BĘBEN ODWRACAJACY) - INDEX 14
            ///
            ///Lista shifts(przesunięcia):
            ///index    -   przesunięcie
            ///     1   -   17
            ///     2   -   5
            ///     3   -   22
            ///     4   -   10
            ///     5   -   0
            ///     6   -   13
            ///     7   -   13
            ///     8   -   13
            ///     9   -   0
            ///     10  -   0
            ///</summary>
            //ORYGINALNY ZESTAW ZNAKÓW
            this.rotors.Add("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            //WIRNIK 1;
            this.rotors.Add("EKMFLGDQVZNTOWYHXUSPAIBRCJ");
            //WIRNIK 2;
            this.rotors.Add("AJDKSIRUXBLHWTMCQGZNPYFVOE");
            //WIRNIK 3;
            this.rotors.Add("BDFHJLCPRTXVZNYEIWGAKMUSQO");
            //WIRNIK 4;
            this.rotors.Add("ESOVPZJAYQUIRHXLNFTGKDCMWB");
            //WIRNIK 5;
            this.rotors.Add("VZBRGITYUPSDNHLXAWMJQOFECK");
            //WIRNIK 6;
            this.rotors.Add("JPGVOUMFYQBENHZRDKASXLICTW");
            //WIRNIK 7;
            this.rotors.Add("NZJHGRCXMYSWBOUFAIVLPEKQDT");
            //WIRNIK 8;
            this.rotors.Add("FKQHTLXOCBJSPDZRAMEWNIUYGV");
            //WIRNIK BETA - INDEX 9;
            this.rotors.Add("LEYJVCNIXWPBQMDRTAKZGFUHOS");
            //WIRNIK GAMMA - INDEX 10;
            this.rotors.Add("FSOKANUERHMBTIYCWLQPZXVGJD");
            //BĘBEN ODWRACAJACY B - INDEX 11
            this.rotors.Add("YRUHQSLDPXNGOKMIEBFZCWVJAT");
            //BĘBEN ODWRACAJACY C - INDEX 12
            this.rotors.Add("FVPJIAOYEDRZXWGCTKUQSBNMHL");
            //reflektor B Dünn (BĘBEN ODWRACAJACY) - INDEX 13
            this.rotors.Add("ENKQAUYWJICOPBLMDXZVFTHRGS");
            //reflektor C Dünn (BĘBEN ODWRACAJACY) - INDEX 14
            this.rotors.Add("RDOBJNTKVEHMLFCWZAXGYIPSUQ");
            //tablica polskich znaków
            this.rotors.Add("ĄĆĘŁŃÓŚŹŻ");
            //tablica zamienników polskich znaków
            this.rotors.Add("ACELNOSZZ");
            this.shifts.Add(0);
            this.shifts.Add(17);
            this.shifts.Add(5);
            this.shifts.Add(23);
            this.shifts.Add(10);
            this.shifts.Add(26);
            this.shifts.Add(13);
            this.shifts.Add(13);
            this.shifts.Add(13);
        }
        /// <summary>
        /// Funkcja odszyfrowywuje i zaszyfrowywuje ciąg znaków
        /// </summary>
        /// <param name="tekst">Tekst do zaszyfrowania/odszyfrowania</param>
        /// <param name="w1">Numer wirnika skrajnie prawego.</param>
        /// <param name="w2">Numer wirnika środkowego</param>
        /// <param name="w3">Numer wirnik skrajnie lewego.</param>
        /// <param name="w4">Bęben odwracający</param>
        /// <param name="t1">Ustawienie wirnika skrajnie prawego.</param>
        /// <param name="t2">Ustawienie wirnika środkowego.</param>
        /// <param name="t3">Ustawienie wirnika skrajnie lewego.</param>
        /// <returns>Funkcja zwraca zaszyfrowany/odszyfrowany tekst.</returns> 
        public string Enigma_ED(string tekst, int w1, int w2, int w3, int w4, int t1, int t2, int t3)
        {
            this.initRotors(); 
            int x=0;
            int lng = this.rotors[0].Length;
            string ret = "";
            bool blad = false;
            //t1 = (t1 - 1 +lng)%lng;
            //sprawdzenie czy ustawienia wirników śą poprawne
            if (w1 > 5 || w2 > 5 || w3 > 5 || w4 > 4 || w1 < 1 || w2 < 1 || w3 < 1 || w4 < 1)
            {
                return "ZLE DANE";
            }
            else
            {
                if (w1 == w2 || w1 == w3 || w2 == w3)
                {
                    return "ZLE USTAWIENIE WIRNIKÓW. USTAW TAK ABY SIĘ NIE POWTARZAŁY. NP 1-2-3";
                }
                else
                {
                    //pętla wykonuje się tyle razy ile jest liter w tekscie do zaszyfrowania/odszyfrowania
                    for (int i = 0; i < tekst.Length; i++)
                    {
                        blad = false;
                        //spacje zamienia na "X" w przeciwnym wypadku
                        if (tekst[i] == ' ')
                        {
                            x = this.rotors[0].IndexOf("X");
                        }
                        else
                        {
                            //sprawdza czy tekst[i] jest w talicy znaków w przeciwnym wypadku
                            if (this.rotors[0].Contains(tekst[i].ToString().ToUpper()))
                            {
                                x = this.rotors[0].IndexOf(tekst[i].ToString().ToUpper());
                            }
                            else
                            {
                                //sprawdza czy tekst[i] jest polskim znakiem jeżeli tak zamienia na standardowy ondpowiednik np. Ń->N
                                if (this.rotors[15].Contains(tekst[i].ToString().ToUpper()))
                                {
                                    x = this.rotors[0].IndexOf(this.rotors[16][this.rotors[15].IndexOf(tekst[i].ToString().ToUpper())]);
                                }
                                else
                                //w przeciwnym wypadku zwraca błąd i znak jest pomijany chodzi tu o miedzy innymi znki interpunkcyjne i inne znaki specjalne
                                {
                                    blad = true;
                                }
                            }
                        }
                        //jeżeli znak jest znakiem specjalnym zostaje ominięty
                        if (!blad)
                        {
                            t1++;
                            //obrót trzeciego wirnika jeśli wirnik 2 jest w odpowiedniej pozycji
                            //wirnik 2 obraca się wraz z wirnikiem 3
                            //wirnik 2 - środkowy
                            //wirnik 3 - skrajny lewy
                            if (t2 + 1 == this.shifts[w2])
                            {
                                t3++;
                                t2++;
                                t3 += this.rotors[0].Length;
                                t3 = t3 % this.rotors[0].Length;
                            }
                            //obrót drugiego wirnika jeśli wirnik 1 jest w odpowiedniej pozycji
                            //wirnik 1 - skrajny prawy
                            //wirnik 2 - srodkowy
                            if (t1 == this.shifts[w1])
                            {
                                t2++;
                                t2 += this.rotors[0].Length;
                                t2 = t2 % this.rotors[0].Length;

                            }
                            t1 += this.rotors[0].Length;
                            t1 = t1 % this.rotors[0].Length;
                            //mechanizm zamiany znaków
                            //pierwszy obrót wirnika
                            x = this.rotors[0].IndexOf(this.rotors[w1][(x + t1) % lng]);
                            //drugi obrót wirnika
                            x = this.rotors[0].IndexOf(this.rotors[w2][(x - t1 + t2 + lng) % lng]);
                            //trzeci obrót wirnika
                            x = this.rotors[0].IndexOf(this.rotors[w3][(x - t2 + t3 + lng) % lng]);
                            //W4+10 poniewaz jest pierwszy wirnik odwracajacy jest pod indexem 11;
                            x = this.rotors[0].IndexOf(this.rotors[w4 + 10][(x - t3 + lng) % lng]);
                            //x = this.rotors[0].IndexOf(this.rotors[w4 + 10][(x) % lng]);
                            //trzeci obrót wirnika powrót
                            x = this.rotors[w3].IndexOf(this.rotors[0][(x + t3) % lng]);
                            //drógi obrót wirnika powrót
                            x = this.rotors[w2].IndexOf(this.rotors[0][(x + t2 - t3 + lng) % lng]);
                            //pierwszy obrót wirnika powrót
                            x = this.rotors[w1].IndexOf(this.rotors[0][(x + t1 - t2 + lng) % lng]);
                            x = (x - t1 + lng) % lng;
                            //dopisuje zaszyfrowany/odszyfrowany znak na końcy zmiennej
                            ret += Convert.ToChar(this.rotors[0][x]);
                        }
                    }
                    //zwraca zaszyfrowany tekst
                    return ret;
                }
            }
        } 
        /// <summary>
        /// Funkcja usuwa znaki "X" z odszyfrowaneo tekstu
        /// </summary>
        /// <param name="tekst">Tekst w którym chcemy usunąć znaki "X"</param>
        /// <returns>Funkcja zwraca przetwożony tekst</returns> 
        public string delX(string tekst)
        {
            string ret = "";
            //pętla wykonuje się tyle razy ile jest znaków
            for (int i = 0; i < tekst.Length; i++)
            {
                //jeżeli natrafi na X wtedy zamienia go na spację
                if (tekst[i] == 'X')
                {
                    ret += " ";
                }
                else {
                    ret += Convert.ToChar(tekst[i]);
                }
            }
            //zwraca  przetwożony tekst
            return ret;
        }
    }
}