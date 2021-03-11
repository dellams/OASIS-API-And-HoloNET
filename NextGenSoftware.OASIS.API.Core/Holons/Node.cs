﻿
using NextGenSoftware.OASIS.API.Core.Enums;
using NextGenSoftware.OASIS.API.Core.Interfaces;

namespace NextGenSoftware.OASIS.API.Core.Holons
{
    public class Node : INode
    {
        public string NodeName { get; set; }
        public NodeType NodeType { get; set; }
    }
}