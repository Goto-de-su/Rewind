using UnityEngine;
using System.Collections.Generic;

public class ScheduleItem
{
    private List<Action> actions;
    public List<Action> Actions => this.actions;

    private void SetActionOrder()
    {
        // actionのroleや優先度によって、順番を変える
    }
}
