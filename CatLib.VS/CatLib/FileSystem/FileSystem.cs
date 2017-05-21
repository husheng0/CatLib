﻿/*
 * This file is part of the CatLib package.
 *
 * (c) Yu Bin <support@catlib.io>
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * Document: http://catlib.io/
 */

using System.IO;
using CatLib.API.FileSystem;
using CatLib.Stl;

namespace CatLib.FileSystem
{
    /// <summary>
    /// 文件系统
    /// </summary>
    public sealed class FileSystem : IFileSystem
    {
        /// <summary>
        /// 文件系统适配器
        /// </summary>
        private readonly IFileSystemAdapter adapter;

        /// <summary>
        /// 文件系统
        /// </summary>
        /// <param name="adapter">适配器</param>
        public FileSystem(IFileSystemAdapter adapter)
        {
            Guard.NotNull(adapter, "adapter");
            this.adapter = adapter;
        }

        /// <summary>
        /// 文件或文件夹是否存在
        /// </summary>
        /// <param name="path">文件或文件夹是否存在</param>
        /// <returns>是否存在</returns>
        public bool Exists(string path)
        {
            return adapter.Has(path);
        }

        /// <summary>
        /// 写入数据
        /// 如果数据已经存在则覆盖
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="contents">写入数据</param>
        /// <returns>是否成功</returns>
        public bool Write(string path, byte[] contents)
        {
            return adapter.Write(path, contents);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>读取的数据</returns>
        public byte[] Read(string path)
        {
            return adapter.Read(path);
        }

        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="path">旧的文件/文件夹路径</param>
        /// <param name="newPath">新的文件/文件夹路径</param>
        /// <returns>是否成功</returns>
        public bool Rename(string path, string newPath)
        {
            return adapter.Rename(path, newPath);
        }

        /// <summary>
        /// 复制文件或文件夹到指定路径
        /// </summary>
        /// <param name="path">文件或文件夹路径(应该包含文件夹或者文件名)</param>
        /// <param name="copyPath">复制到的路径(不应该包含文件夹或者文件名)</param>
        /// <returns>是否成功</returns>
        public bool Copy(string path, string copyPath)
        {
            return adapter.Copy(path, copyPath);
        }

        /// <summary>
        /// 删除文件或者文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>是否成功</returns>
        public bool Delete(string path)
        {
            return adapter.Delete(path);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>是否成功</returns>
        public bool CreateDir(string path)
        {
            return adapter.CreateDir(path);
        }

        /// <summary>
        /// 获取文件/文件夹属性
        /// </summary>
        /// <param name="path">文件/文件夹路径</param>
        /// <returns>文件/文件夹属性</returns>
        public FileAttributes GetAttributes(string path)
        {
            return adapter.GetAttributes(path);
        }

        /// <summary>
        /// 获取文件/文件夹句柄
        /// </summary>
        /// <param name="path">文件/文件夹路径</param>
        /// <returns>文件/文件夹句柄</returns>
        public IHandler Get(string path)
        {
            if (IsDir(path))
            {
                return new Directory(this, path);
            }
            return new File(this, path);
        }

        /// <summary>
        /// 是否是文件夹
        /// </summary>
        /// <param name="path">文件/文件夹路径</param>
        /// <returns>是否是文件夹</returns>
        private bool IsDir(string path)
        {
            return (GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;
        }
    }
}