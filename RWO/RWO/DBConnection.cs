using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RWO
{

    public abstract class User
    {
        public long id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public User (
                long id, string name, 
                string surname, string email
            )
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
        }
    }

    public class UserReader : User
    {
        public string date_of_born { get; set; }
        public string role = "Читатель";
        public UserReader(
                long id, string name,
                string surname, string email, string date_of_born
            ) : base(
                    id, name,
                    surname, email
                )
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.date_of_born = date_of_born;
        }
    }

    public class UserWritter : User
    {
        public string work_experience { get; set; }
        public string role = "Писатель";
        public UserWritter(
                long id, string name,
                string surname, string email, string work_experience
            ) : base(
                    id, name,
                    surname, email
                )
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.work_experience = work_experience;
        }
    }

    public class UserOffer : User
    {
        public string note { get; set; }
        public string role = "Инвестор";
        public UserOffer(
                long id, string name,
                string surname, string email, string note
            ) : base(
                    id, name,
                    surname, email
                )
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.note = note;
        }
    }
    class DBConnection
    {
        User user = null;
        private readonly string URI = "http://176.100.0.104:3001";
        // Все методы класса возвращают строку с ответом на запрос или null - если возникла ошибка при подключении
        // Поле для отладки, -2 - проблемы при подключении, -1 - ошибка доступа, 1 - запрос был выполнен успешно
        public string ExceptionMessage { get; private set; }

        // Метод получения JSON с сервера по адресу запроса (adr)
        public string GetJSON(string adr)
        {
            try
            {
                WebRequest request = WebRequest.Create(URI + adr);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                string answer = new StreamReader(response.GetResponseStream()).ReadToEnd();
                ExceptionMessage = answer.Contains("error") ? "База данных отключена" : null;
                string IsJson = answer.Contains('{') && answer.Contains('}') ? answer : null;
                if (IsJson != null)
                {
                    return answer;
                }
                return null;
            }
            catch (Exception e)
            {
                ExceptionMessage = e.Message;
                return e.Message;

            }
        }
        // метод отправки JSON на сервер, adr - параметр запроса, str - JSON тело запроса
        public User PostUserJson(string adr, string DataString, string role)
        {
            try
            {
                WebRequest request = WebRequest.Create(URI + adr);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(DataString);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse GetRes = request.GetResponse();
                var response = new StreamReader(GetRes.GetResponseStream()).ReadToEnd();
                if (((HttpWebResponse)GetRes).StatusDescription == "OK")
                {
                    ExceptionMessage = response.Contains("error") ? "База данных отключена" : null;
                    string msg = "Вы не подтвердили почту. " +
                        "Вам нужно подтвердить почту, " +
                        "если не прошло более 5 часов с момента регистрации";
                    ExceptionMessage = response.Contains("confirm") ? msg : null;
                    string IsJson = response.Contains('{') && response.Contains('}') ? response : null;
                    if (IsJson != null)
                    {
                        return FillUserClass(IsJson, role);
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                // Запись сообщения об ошибки для отладки
                ExceptionMessage = e.Message;
                return null;

            }
        }

        public string PostJson(string adr, string DataString)
        {
            try
            {
                WebRequest request = WebRequest.Create(URI + adr);
                request.Method = "POST";
                byte[] byteArray = Encoding.UTF8.GetBytes(DataString);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse GetRes = request.GetResponse();
                var response = new StreamReader(GetRes.GetResponseStream()).ReadToEnd();
                if (((HttpWebResponse)GetRes).StatusDescription == "OK")
                {
                    ExceptionMessage = response.Contains("database") ? "База данных отключена" : null;
                    ExceptionMessage = response.Contains("mail") ? "Неверная почта" : null;
                    string IsJson = response.Contains('{') && response.Contains('}') ? response : null;
                    if (IsJson != null)
                    {
                        return "Успешно";
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                // Запись сообщения об ошибки для отладки
                ExceptionMessage = e.Message;
                return "Ошибка запроса к серверу";

            }
        }
        private User FillUserClass(string data, string role)
        {
            try
            {
                if (role == "Читатель")
                {
                    user = JsonSerializer.Deserialize<UserReader>(data);
                }
                else if (role == "Писатель")
                {
                    user = JsonSerializer.Deserialize<UserWritter>(data);
                }
                else if (role == "Инвестор") 
                {
                    user = JsonSerializer.Deserialize<UserOffer>(data);
                }
                return user;
            }
            catch (FormatException e)
            {
                // TODO: отправка внутренней ошибки на сервер
                ExceptionMessage = e.Message;
                return null;
            }
        }
    }
}