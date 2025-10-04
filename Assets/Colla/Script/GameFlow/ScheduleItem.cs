using UnityEngine;
using System.Collections.Generic;

public class ScheduleItem
{
    private List<Act> actions;
    public List<Act> Actions => this.actions;

    private void SetActionOrder()
    {
        // actionのroleや優先度によって、順番を変える
    }
}
