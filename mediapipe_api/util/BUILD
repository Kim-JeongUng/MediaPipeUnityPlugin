# Copyright (c) 2021 homuler
#
# Use of this source code is governed by an MIT-style
# license that can be found in the LICENSE file or at
# https://opensource.org/licenses/MIT.

load("@rules_pkg//pkg:mappings.bzl", "pkg_files")
load("//mediapipe_api:csharp_proto_src.bzl", "csharp_proto_src")

package(
    default_visibility = ["//visibility:public"],
)

cc_library(
    name = "resource_util",
    srcs = ["resource_util_custom.cc"],
    hdrs = ["resource_util_custom.h"],
    deps = [
        "//mediapipe_api:common",
        "@com_google_absl//absl/strings",
        "@com_google_mediapipe//mediapipe/framework/port:ret_check",
        "@com_google_mediapipe//mediapipe/framework/port:status",
        "@com_google_mediapipe//mediapipe/util:resource_util",
    ],
    alwayslink = True,
)

pkg_files(
    name = "proto_srcs",
    srcs = [
        ":color_cs",
        ":render_data_cs",
    ],
    prefix = "Util",
)

csharp_proto_src(
    name = "color_cs",
    proto_src = "mediapipe/util/color.proto",
    deps = [
        "@com_google_mediapipe//mediapipe/util:protos_src",
    ],
)

csharp_proto_src(
    name = "render_data_cs",
    proto_src = "mediapipe/util/render_data.proto",
    deps = [
        "@com_google_mediapipe//mediapipe/util:protos_src",
    ],
)
