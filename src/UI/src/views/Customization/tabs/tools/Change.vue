<template>
  <div class="form__sex">
    <div class="form__box">Choose the floor of the character</div>
    <div
      class="form__sex_select"
      style="display: flex; justify-content: space-between; width: 58%"
    >
      <input
        type="radio"
        class="sex"
        id="male"
        name="sex"
        @click="$emit('change', true)"
        :checked="gender"
      />
      <label for="male" id="male">{{ btnFirst }}</label>
      <input
        type="radio"
        class="sex"
        id="female"
        name="sex"
        @click="$emit('change', false)"
        :checked="!gender"
      />
      <label for="female" id="female">{{ btnSecond }}</label>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    btnFirst: String,
    btnSecond: String,
    gender: Boolean
  },
  mounted(){
    console.log(this.gender)
  }
};
</script>