<template>
  <TabsTemplate>
    <List2
      :itemData="itemData"
      @onSelectSubcategory="(idx) => $emit('onSelectSubcategory', idx)"
      :list="['Makeup', 'Blush', 'Pomade']"
      :active="subcategory"
    />
    <Color
      :color="colors"
      :activeItem="itemData.color1"
      @onSelect="selectColor1"
       title="Basic Farbe"
    />
    <Color
      :color="colors"
      :activeItem="itemData.color2"
      @onSelect="selectColor2"
      title="Zusätzlich Farbe"
    />
  </TabsTemplate>
</template>

<script>
import TabsTemplate from "./TabsTemplate.vue";
import List2 from "./tools/List2.vue";
import Color from "./tools/Color.vue";

export default {
  components: { TabsTemplate, List2, Color },
  props: {
    itemData: Object,
    subcategory: Number,
  },
  data() {
    return {
      colors: [
        { value: 0, color: "#992532" },
        { value: 1, color: "#C8395D" },
        { value: 3, color: "#B8637A" },
        { value: 5, color: "#B1434C" },
        { value: 6, color: "#7F3133" },
        { value: 8, color: "#C18779" },
        { value: 10, color: "#C6918F" },
        { value: 11, color: "#AB6F63" },
        { value: 61, color: "#826355" },
        { value: 15, color: "#CA7F92" },
        { value: 18, color: "#DE3E81" },
        { value: 21, color: "#4F1F2A" },
        { value: 23, color: "#DE2034" },
        { value: 27, color: "#C227B2" },
        { value: 35, color: "#1E74BB" },
        { value: 39, color: "#27C07D" },
      ],
    };
  },
  methods: {
    selectColor1(index, color) {
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