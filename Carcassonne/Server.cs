﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Carcassonne
{
    public class Server
    {
        static TcpListener tcpListener;                  // сервер для прослушивания
        List<Client> clients = new List<Client>();       // все подключения
        public bool isAlive = true;

        protected internal void AddConnection(Client clientObject)
        {
            if (clients.Count < GameGlobals.MAXPLAYERS)
                clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            Client client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
            {
                clients.Remove(client);
            }
        }


        // прослушивание входящих подключений
        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, GameGlobals.PORT);
                tcpListener.Start();
                GameGlobals.chat += "Server started at "+ GameGlobals.LocalIPAddress() +"\r\n";

                while (isAlive)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    Client clientObject = new Client(tcpClient, this);
                    Task.Factory.StartNew(clientObject.Process);
                }
            }
            catch (Exception ex) { Disconnect(); }
        }

        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Stream.Write(data, 0, data.Length); //передача данных
            }
        }

        // трансляция сообщения подключенным клиентам
        protected internal void BroadcastMessage(Cell cell, string id)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            for (int i = 0; i < clients.Count; i++)
            {
                formatter.Serialize(clients[i].Stream, cell);
            }
        }


        // отключение всех клиентов
        protected internal void Disconnect()
        {
            tcpListener.Stop(); //остановка сервера

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close(); //отключение клиента
            }
            clients.Clear();
            isAlive = false;
        }
    }
}
