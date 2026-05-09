<template>
  <div :class="className">
    <div class="form__box">
      <h3>{{ title }}</h3>
    </div>
    <div class="form__data">
      <input
        type="text"
        class="form__input"
        :placeholder="placeholder"
        :value="val"
        @input="(e) => $emit('change', e.target.value)"
        maxlength="15"
      />
    </div>
  </div>
</template>

<script>
export default {
  props: {
    className: String,
    title: String,
    placeholder: String,
    val: String,
  },
};
</script>