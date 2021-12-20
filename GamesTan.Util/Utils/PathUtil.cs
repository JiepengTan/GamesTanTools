// author : https://github.com/JiepengTan
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GamesTan.Util {
    public static partial class PathUtil {
        private static string _relPath;
        private static string RelPath {
            get {
                if (_relPath == null) {
                    _relPath = Application.dataPath.Substring(0, Application.dataPath.Length - 7);
                }

                return _relPath;
            }
        }
        public static void EnsureDirectoryExist(string path) {
            if (path.StartsWith(RelPath)) {
                path = GetFullPath(path);
            }

            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetAbsPath(string assetPath) {
            assetPath = assetPath.Replace("\\", "/");
            var paths = assetPath.Split('/');
            List<string> strs = new List<string>();
            foreach (var path in paths) {
                if (path == ".") {
                    continue;
                }

                if (path == "..") {
                    try {
                        strs.RemoveAt(strs.Count - 1);
                    } catch (Exception e) {
                        Console.WriteLine(e);
                        throw;
                    }

                    continue;
                }

                strs.Add(path);
            }

            var finalPath = string.Join("/", strs);
            return finalPath;
        }

        public static string GetFullPath(string assetPath) {
            return Path.Combine(RelPath, assetPath);
        }

        public static string GetParentDir(string dir) {
            dir = dir.Replace("\\", "/");
            if (dir.EndsWith("/")) {
                dir = dir.Substring(0, dir.Length - 1);
            }

            dir = dir.Substring(0, dir.LastIndexOf("/"));
            return dir;
        }

        public static string GetAssetPath(string fullPath) {
            fullPath = fullPath.Replace("\\", "/");
            if (fullPath.StartsWith(RelPath)) {
                return fullPath.Substring(RelPath.Length + 1);
            }

            return fullPath;
        }

        public static void Walk(string path, string exts, System.Action<string> callback, bool isIncludeDir = false,
            bool isSaveAssets = false,
            bool isAllDirs = true) {
            bool isAll = string.IsNullOrEmpty(exts) || exts == "*" || exts == "*.*";
            string[] extList = exts.Replace("*", "").Split('|');

            if (Directory.Exists(path)) {
                List<string> paths = new List<string>();
                string[] lst = exts.Split('|');
                foreach (var ext in lst) {
                    // 如果选择的是文件夹
                    SearchOption searchOption =
                        isAllDirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    string[] files = Directory.GetFiles(path, ext, searchOption);
                    paths.AddRange(files);
                    if (isIncludeDir) {
                        string[] directories = Directory.GetDirectories(path, ext, searchOption);
                        paths.AddRange(directories);
                    }
                }

                foreach (var item in paths) {
                    callback?.Invoke(item);
                }

                if (isSaveAssets) { }
            } else {
                if (isAll) {
                    callback?.Invoke(path);
                } else {
                    foreach (var ext in extList) {
                        if (path.EndsWith(ext, StringComparison.OrdinalIgnoreCase)) {
                            callback?.Invoke(path);
                        }
                    }
                }
                if (isSaveAssets) { }
            }
        }
    }
}