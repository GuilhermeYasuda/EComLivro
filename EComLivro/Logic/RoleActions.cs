using EComLivro.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EComLivro.Logic
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            // Acessa o contexto da aplicação e cria as variáveis result
            Models.ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult IdRoleResult;
            IdentityResult IdUserResult;

            // Cria a RoleStore usando o contexto
            // A RoleStore só é permitida conter objetos IdentityRole
            var roleStore = new RoleStore<IdentityRole>(context);

            // Cria uma objeto RoleManager onde é apenas permitida conter objetos IdentityRole
            // Quando criado a objeto RoleManager, você passa (como parâmetro) um novo objeto RoleStore 
            var roleMgr = new RoleManager<IdentityRole>(roleStore);

            // Em seguida, você cria a regra "canEdit" se ela ainda não existir
            if (!roleMgr.RoleExists("canEdit"))
            {
                IdRoleResult = roleMgr.Create(new IdentityRole { Name = "canEdit" });
            }

            // Crie um objeto UserManager com base no objeto UserStore e no objeto ApplicationDbContext
            // object. Observe que você pode criar novos objetos e usá-los como 
            // parâmetros em uma única linha de código, em vez de usar várias
            // linhas de código, como fez para o objeto RoleManager
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "admin@ecomlivro.com.br",
                Email = "admin@ecomlivro.com.br"
            };
            IdUserResult = userMgr.Create(appUser, "Admin123*");

            // Se o novo usuário "admin" foi criado com sucesso, 
            // adicione o usuário "admin" à regra "canEdit". 
            if (!userMgr.IsInRole(userMgr.FindByEmail("admin@ecomlivro.com.br").Id, "canEdit"))
            {
                IdUserResult = userMgr.AddToRole(userMgr.FindByEmail("admin@ecomlivro.com.br").Id, "canEdit");
            }
        }
    }
}