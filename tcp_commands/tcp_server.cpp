//
// Created by kira on 05.10.2019.
//
//
// server.cpp
// ~~~~~~~~~~

#include "tcp_server.h"

int main() {
    unsigned port;
    std::cout << "Usage [port]" << std::endl;
    std::cin >> port;
    tcp_server tcp_cmd(port);
    std::string cmd;
    while(true) {
        tcp_cmd.accept_wait();
        int bytes_read;

        while (true) {
            bytes_read = tcp_cmd >> cmd;
            if (bytes_read <= 0) {
                break;
            }
            std::cout << cmd << std::endl;
            if (cmd == "stop") {
                break;
            }
        }

        if (cmd == "stop") {
            break;
        }
    }
    return 0;
}

tcp_server::tcp_server(unsigned port)
: addr(), sock(), listener(socket(AF_INET, SOCK_STREAM, 0)), size_(128) {
    assert(listener != -1);
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    assert(bind(listener, (struct sockaddr *)&addr, sizeof(addr)) != -1);
    listen(listener, 1);
}

tcp_server::~tcp_server() {
    close(sock);
}

int tcp_server::operator>>(std::string& msg) const {
    char buf[size_];
    int res = recv(sock, buf, size_, 0);
    msg = buf;
    return res;
}

int tcp_server::operator<<(const std::string &msg) const {
    return send(sock, msg.c_str(), msg.size(), 0);
}

int tcp_server::accept_wait() {
    sock = accept(listener, NULL, NULL);
    assert(sock != -1);
    std::cout << "accepted" << std::endl;
    return sock;
}
