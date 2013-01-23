using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZoombuM1Service.DAL.entities;

namespace ZoombuM1Service
{
    [ServiceContract]
    public interface IService1
    {
        //Manage Users
        [OperationContract]
        User Create(User user);
        [OperationContract]
        User GetUserByEmailAndPass(String email, String pass);
        [OperationContract]
        User GetUserById(int id);
        [OperationContract]
        List<User> GetUserByName(String name);
        [OperationContract]
        ICollection<User> GetAllUser();
        [OperationContract]
        int AddUserToGroup(int idUser, int idGroup);
        [OperationContract]
        int RemoveUserFromGroup(int idUser, int idGroup);
        [OperationContract]
        int AddFollow(int idUser, int idUserDestinataire);
        [OperationContract]
        int RemoveFollow(int idUser, int idUserDestinataire);

        //Manage Groups
        [OperationContract]
        Group GetGroupById(int id);
        [OperationContract]
        int DeleteGroup(int id);
        [OperationContract]
        int CreateGroup(Group group);

        //Manage Post
        [OperationContract]
        Post CreatePost(Post post, int idUser, int idGroup);
        [OperationContract]
        ICollection<Post> GetPostByGroup(Group group);
        [OperationContract]
        Post GetPostById(int id);
        [OperationContract]
        ICollection<Post> GetPostByName(String name, User user);
        [OperationContract]
        int DeletePost(int id);
        [OperationContract]
        ICollection<Post> GetPostFollow(int id);
        [OperationContract]
        int AddPictureToAPost(Post post, String pathPicture);

        //Manage Like
        [OperationContract]
        int CreateLike(Like like);
        [OperationContract]
        int DeleteLike(int id);
        [OperationContract]
        Like GetLikeById(int id);

        // Manage Comment
        [OperationContract]
        int CreateComment(Comment comment);
        [OperationContract]
        int DeleteComment(int id);
        [OperationContract]
        Comment GetCommentById(int id);
    }
}
