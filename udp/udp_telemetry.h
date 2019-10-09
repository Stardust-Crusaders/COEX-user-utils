//
// Created by kira on 07.10.2019.
//

#ifndef OUTER_WORLD_UDP_RECEIVER_H
#define OUTER_WORLD_UDP_RECEIVER_H

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <cstdlib>
#include <unistd.h>
#include <iostream>
#include <string>
#include <cassert>
#include "telemetry_template.h"


class udp_receiver {
public:
    explicit udp_receiver(const unsigned &port);
    ~udp_receiver();
    int operator>>(tele_msg &msg) const;
    int operator<<(tele_msg &msg) const;

private:
    struct sockaddr_in addr;
    int sock;
    size_t size_;
};

#endif //OUTER_WORLD_UDP_RECEIVER_H
