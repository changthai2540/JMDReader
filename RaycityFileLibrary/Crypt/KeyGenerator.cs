﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;
using Raycity.File;

namespace Raycity.Crypt
{
    public static class KeyGenerator
    {

        
        /// <summary>
        /// 用於解碼第二部分之金鑰
        /// </summary>
        /// <param name="FileName">不含副檔名的檔名</param>
        /// <returns></returns>
        public static uint GetHeaderKey(string FileName)
        {
            byte[] stringData = Encoding.GetEncoding("UTF-16").GetBytes(FileName);
            return Adler.Adler32(0, stringData, 0, stringData.Length) + 0x3de90dc3;
        }

        public static uint GetDataKey(uint HeaderKey, JMDPackedFileInfo info)
        {
            byte[] strData = Encoding.GetEncoding("UTF-16").GetBytes(info.FileName);
            uint key = Adler.Adler32(0, strData, 0, strData.Length);
            key += info.Ext;
            key += (HeaderKey - 0x7E2AF33D);
            return key;
        }

        public static uint GetDictionaryDataKey(uint HeaderKey)
        {
            return HeaderKey - 0x41014EBF;
        }
    }
}
