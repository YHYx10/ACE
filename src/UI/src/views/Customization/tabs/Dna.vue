<template>
  <TabsTemplate>
    <Slider
      v-for="(item, index) in itemData"
      :key="index"
      :itemData="item"
      :text="item.title"
    />
  </TabsTemplate>
</template>

<script>
import TabsTemplate from "./TabsTemplate.vue";
import Slider from "./tools/Slider.vue";

export default {
  name: "Info",
  props: {
    itemData: Array
  },
  components: { TabsTemplate, Slider },
};
</script>