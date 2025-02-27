﻿using System.Numerics;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void DisconnectPlayer(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.disconnectPlayer))
            {
                _packet.Write(_player.id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void SetName(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.setName))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);

                SendTCPDataToAll(_packet);
            }
        }

        public static void StartGame(float _duration, int _mapId, int? _toClient = null)
        {
            using (Packet _packet = new Packet((int)ServerPackets.startGame))
            {
                _packet.Write(_duration);
                _packet.Write(_mapId);

                if (_toClient == null)
                {
                    SendTCPDataToAll(_packet);
                } else {
                    SendTCPData((int) _toClient, _packet);
                }
            }
        }

        public static void EndGame()
        {
            using (Packet _packet = new Packet((int)ServerPackets.endGame))
            {
                SendTCPDataToAll(_packet);
            }
        }

        public static void GameTime(int _time)
        {
            using (Packet _packet = new Packet((int) ServerPackets.gameTime))
            {
                _packet.Write(_time);

                SendUDPDataToAll(_packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);

                SendUDPDataToAll(_packet);
            }
        }

        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);
            }
        }

        public static void PlayerRespawned(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRespawned))
            {
                _packet.Write(_player.id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void PlayerHit(Player _player, Player _by)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerHit))
            {
                _packet.Write(_player.id);
                _packet.Write(_by.id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void PlayerAmmo(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.PlayerAmmo))
            {
                _packet.Write(_player.myAmmo);

                SendTCPData(_player.id, _packet);
            }
        }

        public static void ItemSpawned(Item _item, int? _toClient = null)
        {
            using (Packet _packet = new Packet((int)ServerPackets.itemSpawned))
            {
                _packet.Write(_item.itemId);
                _packet.Write(_item.position);
                _packet.Write((int)_item.type);

                if (_toClient == null)
                {
                    SendTCPDataToAll(_packet);
                } else {
                    SendTCPData((int) _toClient, _packet);
                }
            }
        }

        public static void ItemPickedUp(Item _item, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.itemPickedUp))
            {
                _packet.Write(_item.itemId);
                _packet.Write(_player.id);

                SendTCPDataToAll(_packet);
            }
        }

        public static void ProjectileSpawned(Projectile _projectile, int? _toClient = null)
        {
            using (Packet _packet = new Packet((int)ServerPackets.projectileSpawned))
            {
                _packet.Write(_projectile.projectileId);
                _packet.Write(_projectile.position);
                _packet.Write(_projectile.owner.id);

                if (_toClient == null)
                {
                    SendTCPDataToAll(_packet);
                } else {
                    SendTCPData((int) _toClient, _packet);
                }
            }
        }

        public static void ProjectilePosition(Projectile _projectile)
        {
            using (Packet _packet = new Packet((int)ServerPackets.projectilePosition))
            {
                _packet.Write(_projectile.projectileId);
                _packet.Write(_projectile.position);

                SendUDPDataToAll(_packet);
            }
        }

        public static void ProjectileDestroy(Projectile _projectile)
        {
            using (Packet _packet = new Packet((int)ServerPackets.projectileDestroyed))
            {
                _packet.Write(_projectile.projectileId);

                SendTCPDataToAll(_packet);
            }
        }

        #endregion
    }
}
