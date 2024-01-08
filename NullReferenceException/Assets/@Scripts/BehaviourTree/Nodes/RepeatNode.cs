using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : INode
{
    private readonly INode _childs;

    public RepeatNode(INode childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_childs == null)
            return INode.ENodeState.ENS_Failure;

       
        switch (_childs.Evaluate())
        {
            case INode.ENodeState.ENS_Running:
                return INode.ENodeState.ENS_Running;
            case INode.ENodeState.ENS_Success:
                return INode.ENodeState.ENS_Success;
            case INode.ENodeState.ENS_Failure:
                // Evaluate();
                return _childs.Evaluate();
        }


        return INode.ENodeState.ENS_Running;
    }
}
