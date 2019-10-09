//
// Created by kira on 07.10.2019.
//
//
// udp receiver for telemetry debug
//

#include "udp_telemetry.h"


int main(int argc, char **argv) {
    if (argc != 2) {
        std::cout << "Usage: [port]" << std::endl;
        return 1;
    }
    unsigned port = std::atoi(argv[1]);
    udp_receiver udp_telemetry(port);
    tele_msg msg = {};
    while ((udp_telemetry >> msg) != -1) {
        std::cout << "x: "     << msg.x     << std::endl
                  << "y: "     << msg.y     << std::endl
                  << "z: "     << msg.z     << std::endl
                  << "yaw: "   << msg.yaw   << std::endl
                  << "pitch: " << msg.pitch << std::endl
                  << "roll: "  << msg.roll  << std::endl;
//        usleep(200000); //  TODO(UsatiyNyan): should we use delays, does UDP block shit? it does!
    }
    return 0;
}

udp_receiver::udp_receiver(const unsigned &port)
: addr(), sock(socket(AF_INET, SOCK_DGRAM, 0)), size_(sizeof(tele_msg)) {
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    addr.sin_addr.s_addr = INADDR_ANY;
    assert(bind(sock, (struct sockaddr *)&addr, sizeof(addr)) != -1);
    assert(sock != -1);
}

udp_receiver::~udp_receiver() {
    close(sock);
}

int udp_receiver::operator>>(tele_msg &msg) const {
    char buf[size_];
    int res = recvfrom(sock, buf, size_, 0, NULL, NULL);
    msg = *(tele_msg *)buf;
    return res;
}

int udp_receiver::operator<<(tele_msg &msg) const {
    return sendto(sock, (void *)&msg, size_, 0, (struct sockaddr *)&addr, sizeof(addr));
}
