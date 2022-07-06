using Business.Base;
using Common;
using DataAccess.Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Businesses
{
    public class PostBusiness : BaseBusiness<Post>
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostBusiness(IUnitOfWork unitofwork) : base(unitofwork, unitofwork.PostRepository!)
        {
            _unitOfWork = unitofwork;
        }
         
        public async Task<List<Post>> DisplayAllPostByUserIDAsync(int userid , CancellationToken cancellationToken= new())=>
            await _unitOfWork.PostRepository.DisplayAllPostByUserIDAsync(userid, cancellationToken);

        public async Task<Post> DiplayPostByIDAsync (int id , CancellationToken cancellationToken= new())=>
            await _unitOfWork.PostRepository.DiplayPostByIDAsync(id, cancellationToken);

     
    }
}
