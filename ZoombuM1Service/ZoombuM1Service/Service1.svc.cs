using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZoombuM1Service.DAL.entities;
using ZoombuM1Service.DAL.repositories;

namespace ZoombuM1Service
{
    public class Service1 : IService1
    {
        // Manage Users //
        public User Create(User user)
        {
            return User_Repo.Create(user);
        }

        public User GetUserByEmailAndPass(string email, string pass)
        {
            return User_Repo.GetUserByEmailAndPass(email, pass);
        }

        public User GetUserById(int id)
        {
            return User_Repo.GetUserById(id);
        }

        public ICollection<User> GetAllUser()
        {
            return User_Repo.GetAllUser();
        }

        public List<User> GetUserByName(string name)
        {
            return User_Repo.GetUserByName(name);
        }

        public int AddUserToGroup(int idUser, int idGroup)
        {
            return User_Repo.AddUserToGroup(idUser, idGroup);
        }

        public int RemoveUserFromGroup(int idUser, int idGroup)
        {
            return User_Repo.RemoveUserFromGroup(idUser, idGroup);
        }

        public int AddFollow(int idUser, int idUserDestinataire)
        {
            return User_Repo.AddFollow(idUser, idUserDestinataire);
        }

        public int RemoveFollow(int idUser, int idUserDestinataire)
        {
            return User_Repo.RemoveFollow(idUser, idUserDestinataire);
        }

        // Manage Groups //
        public Group GetGroupById(int id)
        {
            return Group_Repo.GetGroupById(id);
        }

        public int DeleteGroup(int id)
        {
            return Group_Repo.DeleteGroup(id);
        }

        public int CreateGroup(Group group)
        {
            return Group_Repo.Create(group);
        }

        // Manage Posts //
        public Post CreatePost(Post post, int idUser, int idGroup)
        {
            return Post_Repo.Create(post, idUser, idGroup);
        }

        public ICollection<Post> GetPostByGroup(Group group)
        {
            return Post_Repo.GetPostByGroup(group);
        }

        public Post GetPostById(int id)
        {
            return Post_Repo.GetPostById(id);
        }

        public ICollection<Post> GetPostByName(string name, User user)
        {
            return Post_Repo.GetPostByName(name, user);
        }

        public int DeletePost(int id)
        {
            return Post_Repo.Delete(id);
        }

        public ICollection<Post> GetPostFollow(int id)
        {
            return Post_Repo.GetPostFollow(id);
        }

        public int AddPictureToAPost(Post post, string pathPicture)
        {
            return Post_Repo.AddPictureToAPost(post, pathPicture);
        }

        // Manage Likes //
        public int CreateLike(Like like)
        {
            return Like_Repo.Create(like);
        }

        public int DeleteLike(int id)
        {
            return Like_Repo.Delete(id);
        }

        public Like GetLikeById(int id)
        {
            return Like_Repo.GetLikeById(id);
        }

        // Manage Comments //
        public int CreateComment(Comment comment)
        {
            return Comment_Repo.Create(comment);
        }

        public int DeleteComment(int id)
        {
            return Comment_Repo.Delete(id);
        }

        public Comment GetCommentById(int id)
        {
            return Comment_Repo.GetCommentById(id);
        }

    }
}
