﻿using System;
using System.Collections.Generic;
using System.Linq;
using MvcWhatsUp.Models;

namespace MvcWhatsUp.Repositories
{
    public class DummyUsersRepository : IUsersRepository
    {
        private List<Models.User> users = new List<Models.User>
        {
            new Models.User(1, "Shadow", "0666123456", "ultimate_lifeform@chaos.gov", "Maria4Ever"),
            new Models.User(2, "Sonic", "0666987654", "gotta_go_fast@blueblur.com", "ChiliDogLover"),
            new Models.User(3, "Knuckles", "0666554433", "guardian@angelisland.net", "MasterEmerald123"),
            new Models.User(4, "Tails", "0666778899", "two_tails@miles.com", "FlyHigh123"),
            new Models.User(5, "Amy", "0666112233", "piko_hammer@heartbreak.com", "SonicMine"),
            new Models.User(6, "Dr. Eggman", "0666000001", "evil_genius@eggman.net", "WorldDomination"),
            new Models.User(7, "Rouge", "0666998877", "gem_hunter@treasure.com", "ShinyThings"),
            new Models.User(8, "Silver", "0666121212", "future_hero@psychic.com", "SaveTheFuture"),
            new Models.User(9, "Blaze", "0666345678", "princess@sol.com", "Pyrokinesis"),
            new Models.User(10, "Cream", "0666789123", "sweet_rabbit@cheese.com", "ChaoLover")
        };

        public List<Models.User> GetAll()
        {
            return users;
        }

        public Models.User? GetById(int userId)
        {
            return users.FirstOrDefault(x => x.UserId == userId);
        }

        public void Add(User user)
        {
            if (users.Any(u => u.UserId == user.UserId))
            {
                throw new InvalidOperationException("User with the same ID already exists. Chaos Control failed!");
            }
            users.Add(user);
        }

        public void Update(User user)
        {
            var existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException("Agent not found. Perhaps they vanished into the Chaos Void.");
            }
            existingUser.UserName = user.UserName;
            existingUser.MobileNumber = user.MobileNumber;
            existingUser.EmailAddress = user.EmailAddress;
            existingUser.Password = user.Password;
        }

        public void Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "Agent object cannot be null.");
            }

            var existingUser = users.FirstOrDefault(u => u.UserId == user.UserId);
            if (existingUser != null)
            {
                users.Remove(existingUser);
            }
            else
            {
                throw new InvalidOperationException("Agent not found. They’ve gone rogue and disappeared into the shadows.");
            }
        }
    }
}