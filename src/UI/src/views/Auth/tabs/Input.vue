<template>
  <div class="form-input">
    <span>{{ title }}</span>
    <input
      :type="type"
      :value="value"
      :placeholder="placeholder"
      @input="$emit('update', $event.target.value)"
    />
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    type: String,
    placeholder: String,
    value: String,
  },
  model: {
    prop: "value",
    event: "update",
  },
};
</script>