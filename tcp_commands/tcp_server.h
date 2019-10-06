//
// Created by kira on 05.10.2019.
//

#ifndef CATKIN_WS_TCP_SERVER_H
#define CATKIN_WS_TCP_SERVER_H

#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <iostream>
#include <string>
#include <cassert>

class tcp_server {
public:
    tcp_server(unsigned port);
    ~tcp_server();
    int accept_wait();
    int operator>>(std::string& msg) const;
    int operator<<(const std::string& msg) const;
private:
    struct sockaddr_in addr;
    int sock;
    int listener;
    size_t size_;
};

#endif //CATKIN_WS_TCP_SERVER_H
