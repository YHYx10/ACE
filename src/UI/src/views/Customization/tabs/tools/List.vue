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
    <div class="form__parents_content">
      <div v-for="(img, index) in itemData.items" :key="index">
        <input
          type="radio"
          class="parent-fa"
          id="parent-f1"
          name="parent-fa"
          :checked="activeItem === index"
          @click="defaultClick"
        />
        <label for="parent-f1" id="parent-f1" @click="onSelect(index)"
          ><img
            :src="`/img/customization/${folder}/${img}.png`"
            alt=""
            class="p-img"
        /></label>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  props: {
    itemData: Object,
    list: Array,
    active: Number,
    activeItem: Number,
  },
  computed: {
    ...mapState("customization", ["gender"]),
    folder() {
      return this.gender ? this.itemData["folderm"] : this.itemData["folderf"];
    },
  },
  methods: {
    defaultClick(e) {
      e.preventDefault();
    },
    onSelect(index) {
      if (this.itemData.value === index) return;
      this.itemData.value = index;
      this.$emit(
        "onSelect",
        this.itemData.tag,
        this.itemData.items[this.itemData.value]
      );
    },
    selectCategories(index) {
      this.$emit("onSelectSubcategory", index);
    },
  },
};
</script>