//
// Created by kira on 05.10.2019.
//
//
// client.cpp
// ~~~~~~~~~~
//

#include <cstdlib>
#include "tcp_client.h"

int main(int argc, char ** argv) {
    if (argc != 3) {
        std::cout << "Usage: [host] [port]" << std::endl;
        return 1;
    }
    std::string host = argv[1];
    unsigned port = std::atoi(argv[2]);
    tcp_client tcp_cmd(host, port);
    char msg_c;
    std::cout << "start sending commands" << std::endl;
    while (true) {
        std::cin >> msg_c;
        switch (msg_c) {
            case '\0':
                assert ((tcp_cmd << std::to_string(cmd::stop)) != -1);
                break;
            case 'w':
                assert ((tcp_cmd << std::to_string(cmd::step_w)) != -1);
                break;
            case 'a':
                assert ((tcp_cmd << std::to_string(cmd::step_a)) != -1);
                break;
            case 's':
                assert ((tcp_cmd << std::to_string(cmd::step_s)) != -1);
                break;
            case 'd':
                assert ((tcp_cmd << std::to_string(cmd::step_d)) != -1);
                break;
            case 'W':
                assert ((tcp_cmd << std::to_string(cmd::step_shift)) != -1);
                break;
            case 'S':
                assert ((tcp_cmd << std::to_string(cmd::step_ctrl)) != -1);
                break;
            case 'f':
                assert ((tcp_cmd << std::to_string(cmd::flip)) != -1);
                break;
            default:
                std::cout << "incorrect input" << std::endl;
                continue;
        }
    }
    return 0;
}

tcp_client::tcp_client(const std::string& host, unsigned port) : addr(), sock(), size_(128) {
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    addr.sin_addr.s_addr = inet_addr(host.c_str());
    sock = socket(AF_INET, SOCK_STREAM, 0);
    assert(sock != -1);
    assert(connect(sock, (struct sockaddr *) &addr, sizeof(addr)) == 0);
}

tcp_client::~tcp_client() {
    close(sock);
}

int tcp_client::operator>>(std::string& msg) const {
    char buf[size_];
    int res = recv(sock, buf, size_, 0);
    msg = buf;
    return res;
}

int tcp_client::operator<<(const std::string& msg) const {
    return send(sock, msg.c_str(), size_, 0);
}
