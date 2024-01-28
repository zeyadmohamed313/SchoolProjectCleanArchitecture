using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Repositories
{
	public class RefreshTokenRepository:GenericRepositoryAsync<UserRefreshToken>,IRefreshTokenRepository
	{
		#region Fields
		private readonly DbSet<UserRefreshToken> _userRefreshToken;
		#endregion
		#region Constructor
		public RefreshTokenRepository(ApplicationDbContext context) : base(context)
		{
			_userRefreshToken = context.Set<UserRefreshToken>();
		}
		#endregion
		#region Handle Function

		#endregion
	}
}
