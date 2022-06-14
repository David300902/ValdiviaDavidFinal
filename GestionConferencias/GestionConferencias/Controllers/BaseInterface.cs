using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace GestionConferencias.Controllers
{
    public interface BaseInterface<T>
    {
        int Update(T t);
        int Insert(T t);
        int Delete(T t);
        DataTable Select();
    }
}
