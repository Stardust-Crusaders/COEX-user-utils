//
// Created by kira on 05.10.2019.
//

#ifndef OUTER_WORLD_TCP_CLIENT_H
#define OUTER_WORLD_TCP_CLIENT_H

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <cstdlib>
#include <iostream>
#include <string>
#include <cassert>
#include "commands_template.h"

class tcp_client {
public:
    tcp_client(const std::string& host, unsigned port);
    ~tcp_client();
    int operator>>(std::string& msg) const;
    int operator<<(const std::string& msg) const;
private:
    struct sockaddr_in addr;
    int sock;
    size_t size_;
};

#endif //OUTER_WORLD_TCP_CLIENT_H
