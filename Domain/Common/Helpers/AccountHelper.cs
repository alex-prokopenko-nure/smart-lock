﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Common.Helpers
{
    public class AccountHelper
    {
        public static String GetPasswordHash(String password)
        {
            using (var md5 = MD5.Create())
            {
                var resultBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));

                return string.Join(String.Empty, resultBytes.Select(x => x.ToString("X02")));
            }
        }
    }
}
