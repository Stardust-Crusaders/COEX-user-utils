cmake_minimum_required(VERSION 3.0)
project(outer_world VERSION 0.0.0)

set(CMAKE_CXX_STANDARD 17)

add_executable(tcp_commands tcp_commands/tcp_client.cpp
        tcp_commands/tcp_client.h
        tcp_commands/commands_template.h
        )
add_executable(tcp_server tcp_commands/tcp_server.cpp
        tcp_commands/tcp_server.h
        )

add_executable(udp_telemetry udp/udp_telemetry.cpp
        udp/udp_telemetry.h
        udp/telemetry_template.h
        )
