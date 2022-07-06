using Business.Base;
using DataAccess.Base;
using DataAccess.Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Businesses
{
    public class CommentBusiness : BaseBusiness<Comment>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentBusiness(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CommentRepository!)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Comment>> DisplayAllPostCommentsAsync(int postid , CancellationToken cancellationToken= new())=>
            await _unitOfWork.CommentRepository.DisplayAllPostCommentsAsync(postid, cancellationToken);
    }
}
