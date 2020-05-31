using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Carcassonne
{
    public class Client
    {
        protected internal string Id { get; private set; }
        protected internal NetworkStream Stream { get; private set; }
        string userName;
        TcpClient client;
        Server server; // объект сервера
        private bool isAlive = true; 

        public Client(TcpClient tcpClient, Server serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();

                // получаем имя пользователя
                StringBuilder stringBuilder;
                stringBuilder = GetMessage();
                string message = stringBuilder.ToString();
                userName = message;

                message = userName + " joined!";

                // посылаем сообщение о входе в чат всем подключенным пользователям
                server.BroadcastMessage(message, this.Id);

                while (isAlive)
                {
                    try
                    {
                        if (GameGlobals.GAMESTATE == GameGlobals.GameState.chat)
                        {
                            stringBuilder = GetMessage();
                            message = String.Format("{0}: {1}", userName, stringBuilder.ToString());
                            server.BroadcastMessage(message, this.Id);
                        }
                        else
                        if (GameGlobals.GAMESTATE == GameGlobals.GameState.game)
                        {
                            BinaryFormatter formatter = new BinaryFormatter();
                            Cell cell = (Cell)formatter.Deserialize(Stream); 
                        }
                    }
                    catch
                    {
                        message = String.Format("{0}: left chat", userName);
                        server.BroadcastMessage(message, this.Id);
                        break;
                    }
                }
            }
            catch (Exception ex) {}
            finally
            {
                // в случае выхода из цикла закрываем ресурсы
                server.RemoveConnection(this.Id);
                Close();
            }
        }

        // чтение входящего сообщения и преобразование в строку
        private StringBuilder GetMessage()
        {
            byte[] data = new byte[1024];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (Stream.DataAvailable);

            return builder;
        }

        // закрытие подключения
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
            isAlive = false;
        }
    }
}
