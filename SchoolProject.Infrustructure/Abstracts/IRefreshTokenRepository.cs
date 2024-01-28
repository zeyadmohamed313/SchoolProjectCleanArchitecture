using SchoolProject.Data.Entites.Identity;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Abstracts
{
	public interface IRefreshTokenRepository:IGenericRepositoryAsync<UserRefreshToken>
	{

	}
}
