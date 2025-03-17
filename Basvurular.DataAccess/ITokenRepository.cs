using Basvurular.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basvurular.DataAccess
{
    public interface ITokenRepository
    {
        Admins? Authenticate(string adminAd, string adminSifre);

    }
}
