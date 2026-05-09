<template>
  <TabsTemplate>
    <List
      :itemData="itemData"
      @onSelect="selectHair"
      @onSelectSubcategory="(idx) => $emit('onSelectSubcategory', idx)"
      :list="[
        'Hairstyles',
        'Brows',
        'Hair on the face',
        'Hair on the chest',
      ]"
      :active="subcategory"
      :activeItem="itemData.value"
    />
    <Color
      :activeItem="itemData.color1"
      @onSelect="selectColor1"
      :color="hairColors"
      title="Basic Farbe"
    />
    <Color
      :activeItem="itemData.color2"
      @onSelect="selectColor2"
      :color="hairColors"
      title="Zusätzlich Farbe"
    />
  </TabsTemplate>
</template>

<script>
import TabsTemplate from "./TabsTemplate.vue";
import List from "./tools/List.vue";
import Color from "./tools/Color.vue";

export default {
  components: { TabsTemplate, List, Color },
  data() {
    return {
      hairColors: [
        { value: 0, color: "#1C1F21" },
        { value: 3, color: "#35261C" },
        { value: 6, color: "#6D4C35" },
        { value: 8, color: "#765C45" },
        { value: 11, color: "#A79369" },
        { value: 13, color: "#BBA063" },
        { value: 15, color: "#DAC38E" },
        { value: 16, color: "#9F7F59" },
        { value: 19, color: "#61120C" },
        { value: 21, color: "#7C140F" },
        { value: 23, color: "#B64B28" },
        { value: 26, color: "#626262" },
        { value: 28, color: "#AAAAAA" },
        { value: 32, color: "#763C76" },
        { value: 36, color: "#04959E" },
        { value: 40, color: "#217C61" },
      ],
    };
  },
  props: {
    itemData: Object,
    subcategory: Number,
  },
  methods: {
    selectHair(tag, value) {
      window.mp.trigger("customization:update", tag, value);
    },
    selectColor1(index, color) {
      console.log(index);
      this.itemData.color1 = index;
      window.mp.trigger(
        "customization:update",
        `${this.itemData.tag}Color1`,
        color
      );
    },
    selectColor2(index, color) {
      this.itemData.color2 = index;
      window.mp.trigger(
        "customization:update",
        `${this.itemData.tag}Color2`,
        color
      );
    },
  },
};
</script>