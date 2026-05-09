<template>
  <div class="form__color">
    <div class="form__box">
      <h3>
        {{ title }}
      </h3>
    </div>

    <!-- selected - выбранный цвет -->

    <div class="form__selector">
      <slot v-for="(elem, index) in color">
        <input
          type="radio"
          class="color"
          :class="{ selected: activeItem === elem.value }"
          :id="`color-${index}`"
          name="color"
          :key="index"
        />
        <label
          :for="`color-${index}`"
          :id="`color-${index}`"
          :key="'l' + index"
          @click="selectColor(elem.value)"
          :style="{ background: elem.color }"
        ></label>
      </slot>

      <!--<input type="color" id="color1" name="color1" value="#FFFFFF" disabled>
                <input type="color" id="color2" name="color2" value="#FF8F8F" disabled>
                <input type="color" id="color3" name="color3" value="#FF2323" disabled>
                <input type="color" id="color4" name="color4" value="#FFCD4D" disabled>
                <input type="color" id="color5" name="color5" value="#7BFF4D" disabled>
                <input type="color" id="color6" name="color6" value="#4DFF74" disabled>
                <input type="color" id="color7" name="color7" value="#4DFFD4" disabled>
                <input type="color" id="color8" name="color8" value="#4D69FF" disabled>
                <input type="color" id="color9" name="color9" value="#9B4DFF" disabled>
                <input type="color" id="color10" name="color10" value="#0C0C0C" disabled>
                <input type="color" id="color11" name="color11" value="#FF8717" disabled>
                <input type="color" id="color12" name="color12" value="#FFF617" disabled>
                <input type="color" id="color13" name="color13" value="#61FF17" disabled>
                <input type="color" id="color14" name="color14" value="#17F1FF" disabled>
                <input type="color" id="color15" name="color15" value="#172FFF" disabled>
                <input type="color" id="color16" name="color16" value="#6117FF" disabled>
                <input type="color" id="color17" name="color17" value="#D117FF" disabled>
                <input type="color" id="color18" name="color18" value="#FF175D" disabled>-->
    </div>
  </div>
</template>

<script>
export default {
  props: {
    activeItem: Number,
    color: Array,
    title: String,
  },
  methods: {
    selectColor(index) {
      this.$emit("onSelect", index, index);
    },
  },
};
</script>