//
// Created by kira on 07.10.2019.
//

#ifndef OUTER_WORLD_TELEMETRY_TEMPLATE_H
#define OUTER_WORLD_TELEMETRY_TEMPLATE_H

typedef struct tele_msg {
    float x;
    float y;
    float z;
    float yaw;
    float pitch;
    float roll;
    //TODO(UsatiyNyan): should we use velocities?
} tele_msg;


#endif //OUTER_WORLD_TELEMETRY_TEMPLATE_H
