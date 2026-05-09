<template>
  <div class="frac-money">
    <div class="frac-money-bank">
      <div class="frac-money-img">
        <img src="/img/optionsMenu/organizationTab/bank.png">
      </div>
      <div class="frac-money-bank-main">
        <div class="frac-money-bank-heading">
          <div class="category">{{ loc('mmain:frac:info:money:sub') }}</div>
          <div class="title">${{ fraction.bank.toLocaleString() }}</div>
        </div>
        <div class="frac-money-bank-btns">
          <button class="item__btn" @click="depositMoney">{{ loc('mmain:frac:info:money:b2') }}</button>
          <button class="item__btn" :disabled="fraction.bank.toLocaleString().length === 1" @click="withdrawMoney">
            {{ loc('mmain:frac:info:money:b1') }}
          </button>
        </div>
      </div>
    </div>
    <div class="frac-money-common">
      <div class="frac-money-img">
        <img src="/img/optionsMenu/organizationTab/common.png">
      </div>
      <div class="frac-money-common-main">
        <div class="frac-money-common-heading">
          <div class="category">{{ loc('mmain:frac:commom:sub1') }}</div>
          <div class="title">${{ fraction.lastHour.toLocaleString() }}</div>
        </div>
        <div class="frac-money-common-heading">
          <div class="category">{{ loc('mmain:frac:commom:sub2') }}</div>
          <div class="title">${{ fraction.lastDay.toLocaleString() }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState, mapMutations} from 'vuex'

export default {
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('optionsMenu', ['fraction'])
  },
  methods: {
    ...mapMutations('optionsMenu', ['setDialog']),
    withdrawMoney() {
      if (this.lastCheck > Date.now()) return;
      this.lastCheck = Date.now() + 1000;
      if (!this.fraction.canWhithdraw) {
        window.setData('notifyList/notify', {type: 1, position: 2, message: "mmain:frac:nopermission", time: 3000});
        return;
      }
      this.setDialog({
        input: 'number',
        callback: (val) => {
          val = parseInt(val);
          if (isNaN(val) || val < 1) return;
          if (this.fraction.bank < val) {
            window.setData('notifyList/notify', {
              type: 1,
              position: 2,
              message: "mmain:frac:dialog:wmoney:err",
              time: 3000
            });
            return;
          }
          window.mp.triggerServer('fmenu:money:withdraw', +val);
        },
        value: '',
        placeholder: 'mmain:frac:dialog:wmoney:pl',
        tittle: `mmain:frac:dialog:wmoney:tit`,
        subtittle: 'mmain:frac:dialog:wmoney:sub',
        bg: 'invite'
      });
    },
    depositMoney() {
      this.setDialog({
        input: 'number',
        callback: (val) => {
          val = parseInt(val);
          if (isNaN(val) || val < 1) return;
          window.mp.triggerServer('fmenu:money:deposit', +val);
        },
        value: '',
        placeholder: 'mmain:frac:dialog:wmoney:pl',
        tittle: `mmain:frac:dialog:dmoney:tit`,
        subtittle: 'mmain:frac:dialog:wmoney:sub',
        bg: 'invite'
      });
    }
  }
}
</script>

<style lang="scss" scoped>
.frac-money {
  width: 100%;

  &-img {
    width: 8rem;
    margin-right: 2rem;

    & img {
      width: 100%;
      height: 100%;
      object-fit: contain;
    }
  }

  &-bank {
    width: 100%;
    margin-bottom: 3rem;
    display: flex;
    align-items: center;

    .title {
      color: #5CFF80;
    }

    &-btns {
      display: flex;
      align-items: center;
      margin-top: 0.75rem;

      .item__btn:not(.item__btn:first-child) {
        margin-left: 0.5rem;
      }
    }
  }

  &-common {
    width: 100%;
    display: flex;
    align-items: center;

    &-heading:first-child {
      margin-bottom: 1rem;
    }
  }
}
</style>