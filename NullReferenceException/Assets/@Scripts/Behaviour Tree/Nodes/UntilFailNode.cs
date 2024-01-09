using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UntilFailNode : INode
{
    private readonly INode _child;

    public UntilFailNode(INode childs)
    {
        _child = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_child == null)
            return INode.ENodeState.ENS_Failure;


        switch (_child.Evaluate())
        {
            case INode.ENodeState.ENS_Running:
                return INode.ENodeState.ENS_Running;
            case INode.ENodeState.ENS_Success:
                return INode.ENodeState.ENS_Success;
            case INode.ENodeState.ENS_Failure:
                _child.Evaluate();
                break;
        }


        return INode.ENodeState.ENS_Success;
    }
}
