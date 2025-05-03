components {
  id: "moving_pan"
  component: "/scripts/moving_pan.script"
  properties {
    id: "radius"
    value: "100.0"
    type: PROPERTY_TYPE_NUMBER
  }
}
embedded_components {
  id: "sprite"
  type: "sprite"
  data: "default_animation: \"anim\"\n"
  "material: \"/builtins/materials/sprite.material\"\n"
  "size {\n"
  "  x: 0.1\n"
  "  y: 0.15\n"
  "}\n"
  "textures {\n"
  "  sampler: \"texture_sampler\"\n"
  "  texture: \"/assets/tilemaps/pan.tilesource\"\n"
  "}\n"
  ""
  scale {
    x: 0.1
    y: 0.1
  }
}
