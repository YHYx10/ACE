<template>
  <div class="input-block">
    <input
      type="text"
      :placeholder="placeholder"
      :value="value"
      @input="$emit('input', $event.target.value)"
      class="input"
    />
    <div v-show="showButtons" class="confirm">
      <DefaultBtn @click="$emit('onSave')">Save</DefaultBtn>
      <DefaultBtn @click="$emit('onCancel')">Cancel</DefaultBtn>
    </div>
  </div>
</template>

<script>
import DefaultBtn from '../../../UI/button/DefaultBtn.vue';

export default {
    props: {
        value: String,
        placeholder: String,
        showButtons: Boolean,
    },
    components: { DefaultBtn }
}
</script>

<style lang="scss" scoped>
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  color: #ffffff;
}
.input-block {
  position: relative;
  .confirm {
    display: flex;
    justify-content: center;
    z-index: 1;
    position: absolute;
    align-items: center;
    bottom: 0;
    width: 100%;
    gap: 1.019vh;
    width: 38.519vh;
    height: 7.87vh;
    background: #070707;
    transform: translate(0, 100%);
    border: 0.093vh solid rgba(255, 255, 255, 0.09);
    button {
      width: 14.911vh;
      height: 5.559vh;
      font-weight: 700;
      font-size: 1.481vh;
      line-height: 1.759vh;
      text-transform: uppercase;
      &:nth-child(2) {
        background: rgba(255, 255, 255, 0.05);
        &:hover {
          background: rgba(255, 255, 255, 0.12);
        }
      }
    }
  }
}
.input {
  background: rgba(255, 255, 255, 0.04);
  border: 0.093vh solid rgba(255, 255, 255, 0.09);
  font-weight: 700;
  font-size: 2.963vh;
  line-height: 3.519vh;
  text-transform: uppercase;
  padding: 0 3.519vh;
  display: flex;
  align-items: center;
  width: 38.519vh;
  height: 7.87vh;
  font-weight: 700;
  font-size: 2.963vh;
  line-height: 3.519vh;
  text-transform: uppercase;
}
</style>
