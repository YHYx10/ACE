<template>
  

  <div class="bank-modal-fines bank-main-modal">
    <div class="bank-modal-fines__wrap bank-main-modal__wrap">
      <div class="btn-close" @click="$emit('closeModal')"></div>
      <div class="bank-main-modal-title">{{ loc("bank:menu:42") }}</div>
      <div class="bank-main-modal-desc">{{ loc("bank:menu:43") }}</div>
      <div class="bank-modal-fines__list" v-if="finesList.length > 0">
        <div
          class="bank-modal-fines__list-item"
          v-for="item in finesList"
          :key="item.id"
        >
          <div class="bank-modal-fines__list-item-reason">
            {{ loc(item.reason) }}
          </div>
          <div class="bank-modal-fines__list-item__info">
            <div class="bank-modal-fines__list-item__info-cost">
              $ {{ item.cost.toLocaleString("ru") }}
            </div>
            <div class="bank-modal-fines__list-item__info-date">
              {{ item.date }}
            </div>
            <div
              class="bank-modal-fines__list-item__info-btn bank-btn"
              @click="payFine(item.id)"
            >
              {{ loc("bank:menu:44") }}
            </div>
          </div>
        </div>
      </div>
      <div class="bank-modal-fines__desc" v-else>{{ loc("bank:menu:45") }}</div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "BankModalFines",
  props: {
    dataModal: Object,
  },
  computed: {
    ...mapState("bank", ["finesList"]),
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    payFine(id) {
      window.mp.triggerServer("Bank:payFine", id);
    },
  },
};
</script>

<style lang="scss">
.bank {
  .bank-modal-fines {
    .bank-main-modal-title {
      max-width: 22rem;
    }
    .bank-main-modal-title,
    .bank-main-modal-desc {
      padding-left: 1rem;
    }
    &__wrap.bank-main-modal__wrap {
      width: fit-content;
      padding: 2rem 2rem 0.5rem 2rem;
    }
    &__list {
      max-height: 30rem;
      overflow-y: auto;
      padding: 1rem;
      padding-right: 1.35rem;
      margin-right: -1.35rem;
      &::-webkit-scrollbar {
        width: 0.2rem;
        background-color: transparent;
        &-thumb {
          background: #5e37b0;
          border-radius: 0.3rem;
        }
      }
      &-item {
        width: 22rem;
        min-width: 22rem;
        margin-bottom: 0.7rem;
        background: #ffffff;
        box-shadow: 0 0.3rem 1rem rgba(49, 41, 184, 0.2);
        border-radius: 0.5rem;
        padding: 1rem 1.5rem;
        display: flex;
        flex-direction: column;
        &-reason {
          font-size: 1rem;
          line-height: 1.2rem;
          letter-spacing: 0.03em;
          color: #200c49;
          margin-bottom: 0.6rem;
        }
        &__info {
          display: flex;
          align-items: center;
          justify-content: space-between;
          &-cost {
            font-weight: bold;
            font-size: 1.5rem;
            letter-spacing: 0.03em;
            color: #ee443a;
          }
          &-date {
            font-size: 1rem;
            line-height: 1.2rem;
            letter-spacing: 0.03em;
            color: rgba(32, 12, 73, 0.4);
            margin-right: 1.5rem;
          }
          &-btn.btn-main {
            margin-right: -0.5rem;
            margin-bottom: -0.35rem;
            height: 2.4rem;
            padding: 0 1.4rem;
            border: 0.1rem solid #5e37b0;
            border-radius: 5rem;
            background-color: transparent;
            font-weight: normal;
            font-size: 1rem;
            color: #5e37b0;
            &:hover {
              background-color: #5e37b0;
              color: #fff;
            }
          }
        }
        &:last-child {
          margin-bottom: 0;
        }
      }
    }
    &__desc {
      padding: 0 0 2rem 1rem;
      font-size: 2rem;
      line-height: 2.2rem;
      letter-spacing: 0.03em;
      color: #200c49;
    }
  }
}
</style>
