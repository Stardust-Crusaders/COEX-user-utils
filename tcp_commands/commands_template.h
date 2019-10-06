//
// Created by kira on 06.10.2019.
//

#ifndef CATKIN_WS_COMMANDS_TEMPLATE_H
#define CATKIN_WS_COMMANDS_TEMPLATE_H

namespace cmd {
    enum commands {
        stop = 0,
        step_w,
        step_a,
        step_s,
        step_d,
        step_ctrl,
        step_shift,
        vel_w,
        vel_a,
        vel_s,
        vel_d,
    };
}

#endif //CATKIN_WS_COMMANDS_TEMPLATE_H
