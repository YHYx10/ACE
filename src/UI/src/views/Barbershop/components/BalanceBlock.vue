<template>
  <div class="balance">
    <svg
      viewBox="0 0 46 47"
    >
      <circle cx="23" cy="23.5" r="22" stroke="#7DF299" stroke-width="2" />
      <path
        fill="#7DF299"
        d="M15.331 17.78c0-1.26.996-2.28 2.22-2.28h14.435c1.225 0 2.22 1.02 2.22 2.28v7.98c0 1.258-.995 2.28-2.22 2.28H17.552c-1.225 0-2.221-1.022-2.221-2.28v-7.98Zm2.22 7.98h2.221c0-1.257-.995-2.28-2.22-2.28v2.28Zm0-7.98v2.28c1.226 0 2.221-1.022 2.221-2.28h-2.22Zm14.435 5.7c-1.225 0-2.22 1.023-2.22 2.28h2.22v-2.28Zm-2.22-5.7c0 1.258.995 2.28 2.22 2.28v-2.28h-2.22Zm-4.997 1.14c-1.534 0-2.776 1.276-2.776 2.85 0 1.575 1.242 2.85 2.776 2.85 1.534 0 2.776-1.275 2.776-2.85 0-1.574-1.242-2.85-2.776-2.85Zm-11.103 8.266c0 1.418 1.118 2.565 2.498 2.565h13.88c.46 0 .832.381.832.855a.842.842 0 0 1-.833.855h-13.88c-2.299 0-4.163-1.913-4.163-4.275v-8.55c0-.475.373-.856.833-.856.46 0 .832.381.832.855v8.55Z"
      />
    </svg>
    <div>
      <div class="title">Your balance:</div>
      <div class="value">
        {{ (balance || 0).toLocaleString('en-US') }}<span>$</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    balance: Number
  },
  data: () => {
    return {
      
    }
  },
}
</script>

<style lang="scss" scoped>
  
.balance {
  display: flex;
  gap: 1.11vh;
  font-family: 'Montserrat';
  font-style: normal;
  svg {
    width: 4.25vh;
  }
  div {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    gap: 1.01vh;
    height: 5.33vh;
    .title {
      font-weight: 600;
      font-size: 1.48vh;
      line-height: 2.22vh;
      color: rgba(255, 255, 255, 0.24);
    }
    .value {
      display: inline;
      font-family: 'Atomic Age';
      font-style: normal;
      font-weight: 400;
      font-size: 1.85vh;
      line-height: 1.11vh;
      color: #ffffff;
      align-self: flex-start;
      span {
        margin-left: 0.27vh;
        color: rgba(255, 255, 255, 0.22);
      }
    }
  }
}
</style>
