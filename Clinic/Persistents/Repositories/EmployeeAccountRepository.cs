using Clinic.Core;
using Clinic.Models;
using Clinic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Persistents.Repositories
{
    public class EmployeeAccountRepository : IEmployeeAccountRepository
    {
        private readonly ClinicDbContext _context;

        public EmployeeAccountRepository(ClinicDbContext _context)
        {
            this._context = _context;
        }
        public EmployeeAccount Authenticate(string email, string password)
        {

            var userAcc = _context.EmployeeAcounts.Include(x => x.Employee).SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (userAcc == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, userAcc.PasswordHash, userAcc.PasswordSalt))
                return null;

            // authentication successful
            return userAcc;
        }

        public EmployeeAccount Create(EmployeeAccount employeeAccount, string password)
        {

            if (_context.EmployeeAcounts.Any(x => x.Email == employeeAccount.Email))
                throw new Exception("Email \"" + employeeAccount.Email + "\" đã được sử dụng");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            employeeAccount.PasswordHash = passwordHash;
            employeeAccount.PasswordSalt = passwordSalt;

            _context.EmployeeAcounts.Add(employeeAccount);
            _context.SaveChanges();

            return employeeAccount;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
