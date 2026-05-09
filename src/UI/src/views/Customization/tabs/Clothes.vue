<template>
  <TabsTemplate>
    <List
      :itemData="itemData"
      @onSelect="selectClothes"
      @onSelectSubcategory="(idx) => $emit('onSelectSubcategory', idx)"
      :list="['Outerwear', 'Trousers', 'Shoes']"
      :active="subcategory"
      :activeItem="itemData.value"
    />
  </TabsTemplate>
</template>

<script>
import TabsTemplate from "./TabsTemplate.vue";
import List from "./tools/List.vue";

export default {
  components: { TabsTemplate, List },
  props: {
    itemData: Object,
    subcategory: Number,
  },
  methods: {
    selectClothes(tag, value) {
      window.mp.trigger("customization:update", tag, value);
    },
  },
};
</script>