<template>
  <div class="modal-rules">
    <div class="content">
      <Back @click="closeModal" />
      <div class="head">
        <div class="title">Charta der Organisation</div>
        <div class="subtitle">
    Hier können Sie die Hauptregeln und Tabus der Organisation lesen
        </div>
      </div>
      <div class="row">
        <div class="col">
          <div class="col-head">
            <div class="title">Detection</div>
            <div class="edit" @click="editTaboo">
              <img src="/img/familyMenu/edit.svg" alt="" />
              <span>edit</span>
            </div>
          </div>
          <div class="list">
            <div
              class="item"
              v-for="(item, index) in displayTabooList"
              :key="index"
            >
              <img
                v-if="isEditTaboo"
                src="/img/familyMenu/cross.svg"
                class="cross"
                @click="deleteElement(tabooEdit, index)"
              />
              <div v-else class="cross" />
              <div class="rate-position">{{ index + 1 }}.</div>
              <div class="text">
                {{ item.text }}
              </div>
            </div>
          </div>
          <div class="edition">
            <template v-if="isEditTaboo">
              <div class="new-position">
                {{ inputText ? displayTabooList.length + 1 + '.' : '' }}
              </div>
              <div class="block">
                <input
                  v-model="inputText"
                  placeholder="Write to add a item"
                  type="text"
                />
                <div class="inline">
                  <DefaultBtn @click="saveEdit(true)">{{
                    getBtnText
                  }}</DefaultBtn>
                  <DefaultBtn @click="cancelEdit" secondary>
              Hinausgehen
                  </DefaultBtn>
                </div>
              </div>
            </template>
          </div>
        </div>
        <div class="col">
          <div class="col-head">
            <div class="title">Allgemeine Regeln </div>
            <div class="edit" @click="editRules">
              <img src="/img/familyMenu/edit.svg" alt="" />
              <span>bearbeiten</span>
            </div>
          </div>
          <div class="list">
            <div
              class="item"
              v-for="(item, index) in displayRulesList"
              :key="index"
            >
              <img
                v-if="isEditRules"
                src="/img/familyMenu/cross.svg"
                class="cross"
                @click="deleteElement(rulesEdit, index)"
              />
              <div v-else class="cross" />
              <div class="rate-position">{{ index + 1 }}.</div>
              <div class="text">
                {{ item.text }}
              </div>
            </div>
          </div>
          <div class="edition">
            <template v-if="isEditRules">
              <div class="new-position">
                {{ inputText ? displayRulesList.length + 1 + '.' : '' }}
              </div>
              <div class="block">
                <input
                  v-model="inputText"
                  placeholder="Write to add a item"
                  type="text"
                />
                <div class="inline">
                  <DefaultBtn @click="saveEdit(false)">{{
                    getBtnText
                  }}</DefaultBtn>
                  <DefaultBtn @click="cancelEdit" secondary>
       Hinausgehen
                  </DefaultBtn>
                </div>
              </div>
            </template>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import DefaultBtn from '../../UI/button/DefaultBtn.vue'
import Back from '../components/Back.vue'
export default {
  name: 'ModalRules',
  data: function() {
    return {
      currentTextBio: null,
      inputText: '',
      isEditTaboo: false,
      isEditRules: false,
      tabooEdit: [],
      rulesEdit: [],
    }
  },
  computed: {
    ...mapState('familyMenu', ['isLeader', 'infoPage']),
    ...mapGetters('localization', ['loc']),
    displayTabooList() {
      return this.isEditTaboo ? this.tabooEdit : this.infoPage.tabooList
    },
    displayRulesList() {
      return this.isEditRules ? this.rulesEdit : this.infoPage.rulesList
    },
    getBtnText() {
      return this.inputText ? 'Add item ':' Save'
    },
  },
  methods: {
    closeModal: function() {
      this.$emit('closeRulesModal')
    },
    setBio: function() {
      this.closeModal()
      window.mp.trigger('familyMenu:setBio', this.currentTextBio)
    },
    editTaboo() {
      this.cancelEdit()
      this.isEditTaboo = true
      this.tabooEdit = []
      this.copyArray(this.tabooEdit, this.infoPage.tabooList)
    },
    editRules() {
      this.cancelEdit()
      this.isEditRules = true
      this.rulesEdit = []
      this.copyArray(this.rulesEdit, this.infoPage.rulesList)
    },
    copyArray: function(to, from) {
      from.forEach((element) => {
        to.push({ ...element })
      })
    },
    addToArray: function(array) {
      array.push({ text: this.inputText })
      this.inputText = ''
    },
    deleteElement: function(array, index) {
      array.splice(index, 1)
    },
    cancelEdit: function() {
      this.isEditTaboo = false
      this.isEditRules = false
      this.tabooEdit = []
      this.rulesEdit = []
    },
    saveEdit: function(isTaboo) {
      const array = isTaboo ? this.tabooEdit : this.rulesEdit
      if (this.inputText) {
        this.addToArray(array)
        return
      }
      window.mp.trigger(
        'familyMenu:saveEditFamilyRules',
        JSON.stringify(isTaboo ? array : this.infoPage.tabooList),
        JSON.stringify(!isTaboo ? array : this.infoPage.rulesList)
      )
      this.isEditTaboo = false
      this.isEditRules = false
      this.tabooEdit = []
      this.rulesEdit = []
    },
  },
  components: { Back, DefaultBtn },
}
</script>

<style lang="scss" scoped>
$color-red: #301934 ;
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  color: #ffffff;
}
.modal-rules {
  width: 100vw;
  height: 100vh;
  background: #000;
  position: absolute;
  top: 0;
  left: 0;
  z-index: 999;
  display: flex;
  align-items: center;
  justify-content: center;
  .content {
    .head {
      margin-top: 1.389vh;
      .title {
        font-size: 4.444vh;
        line-height: 5.556vh;
      }
      .subtitle {
        margin-top: 0.463vh;
        font-size: 1.852vh;
        line-height: 2.315vh;
      }
    }
  }
}

.modal-rules .content .row {
  display: flex;
  margin-top: 3.241vh;
  gap: 4.907vh;
  .col {
    width: 42.685vh;
    .col-head {
      display: flex;
      justify-content: space-between;
      .title {
        font-size: 3.333vh;
        line-height: 4.167vh;
      }
      .edit {
        margin-right: 0.926vh;
        display: flex;
        gap: 0.741vh;
        align-items: center;
        img {
          width: 1.667vh;
          height: 1.481vh;
        }
        span {
          color: rgba(255, 255, 255, 0.7);
        }
        &:hover {
          text-decoration: underline;
        }
      }
    }
    .list {
      width: calc(100% + 2.778vh);
      display: flex;
      flex-direction: column;
      gap: 1.667vh;
      overflow-y: auto;
      margin-top: 3.056vh;
      height: 39.259vh;
      position: relative;
      transform: translate(-2.778vh);
      &::-webkit-scrollbar {
        width: 0.463vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.04);
      }
      &::-webkit-scrollbar-thumb {
        background: #301934 ;
      }
      .item {
        flex-shrink: 0;
        display: flex;
        gap: 1.667vh;
        font-size: 1.852vh;
        line-height: 2.315vh;
        .cross {
          margin-top: 0.926vh;
          min-width: 0.926vh;
          height: 0.926vh;
          opacity: 0.5;
          &:hover {
            opacity: 1;
          }
        }
        .rate-position {
          color: $color-red;
        }
      }
    }

    .edition {
      margin-top: 1.852vh;
      width: 100%;
      display: flex;
      gap: 0.926vh;
      height: 15.741vh;
      transform: translate(-2.778vh);
      .new-position {
        min-width: 1.852vh;
        text-align: end;
        color: $color-red;
        font-size: 2.222vh;
        line-height: 2.778vh;
      }
      .block {
        display: flex;
        flex-direction: column;
        gap: 0.926vh;
        input {
          width: 42.685vh;
          height: 7.87vh;
          padding: 0 2.685vh;
          background: rgba(255, 255, 255, 0.04);
          border: 0.093vh solid rgba(255, 255, 255, 0.09);
          font-size: 1.852vh;
          line-height: 2.315vh;
          text-transform: uppercase;
          &::placeholder {
            color: #ffffff69;
          }
        }
        .inline {
          display: flex;
          justify-content: space-between;
          button {
            height: 6.944vh;
            width: 13.519vh;
            font-size: 2.222vh;
            line-height: 2.778vh;
            &:first-child {
              width: 27.315vh;
            }
          }
        }
      }
    }
  }
}
</style>
