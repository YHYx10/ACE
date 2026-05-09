<template>
  <TabsTemplate>
    <List
      :itemData="itemData"
      :activeItem="itemData.value"
      @onSelect="selectParent"
      @onSelectSubcategory="selectCategories"
      :list="['Fathers similarities ',' Mothers similarities']"
      :active="subcategory"
    />
    <Slider :itemData="similar" :text="'Form'" />
    <Slider :itemData="skin" :text="'Leather'" />
  </TabsTemplate>
</template>

<script>
import { mapMutations, mapState } from "vuex";
import TabsTemplate from "./TabsTemplate.vue";
import List from "./tools/List.vue";
import Slider from "./tools/Slider.vue";

export default {
  name: "Parents",
  components: { TabsTemplate, List, Slider },
  props: {
    itemData: Object,
    subcategory: Number,
  },
  computed: {
    ...mapState("customization", ["similar", "skin"]),
  },
  methods: {
    ...mapMutations("customization", ["updateSimilar", "updateSkin"]),
    selectParent(tag, value) {
      window.mp.trigger("customization:update", tag, value);
    },
    selectCategories(index) {
      this.$emit("onSelectSubcategory", index);
    },
  }
};
</script>