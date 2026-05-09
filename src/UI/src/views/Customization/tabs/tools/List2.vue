<template>
  <div class="form__parents">
    <div class="form__parents_select">
      <div v-for="(elem, index) in list" :key="index">
        <input
          type="radio"
          class="p-sex"
          id="p-male"
          name="p-sex"
          :checked="active === index"
        />
        <label for="p-male" id="p-male" @click="() => selectCategories(index)">
          <h3>{{ elem }}</h3>
        </label>
      </div>
    </div>

    <div class="form__value">
      <Slider
        div
        class="customiztion-nav-range"
        :min="itemData.min"
        :max="itemData.max"
        :interval="itemData.step"
        v-model="itemData.value"
        @change="onChange"
      />
      <div class="customiztion-nav-range_value">{{ itemData.value }}</div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
import Slider from "vue-slider-component";

export default {
  props: {
    itemData: Object,
    list: Array,
    active: Number,
  },
  components: {
    Slider
  },
  computed: {
    ...mapState("customization", ["gender"]),
    folder() {
      return this.gender ? this.itemData["folderm"] : this.itemData["folderf"];
    },
  },
  methods: {
    selectCategories(index) {
      this.$emit("onSelectSubcategory", index);
    },
    onChange() {
      window.mp.trigger(
        "customization:update",
        this.itemData.tag,
        this.itemData.value
      );
    },
  },
};
</script>