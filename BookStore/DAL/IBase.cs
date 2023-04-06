using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL {
    interface IBase <Type>{
        object getById(int id);
        List<Type> getAll();
        object create(Type type);

        bool delete(Type type);

        bool edit(Type type);
    }
}
