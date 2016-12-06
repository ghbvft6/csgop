using csgop.Imported;
using System;
using System.Diagnostics;

namespace csgop.Unmanaged {

    class AbstractExternalArray<T, BindingClass> where T : struct {

        private AbstractExternal<T, BindingClass>[] array;
        private Values valuesArray;
        

        public AbstractExternalArray(int length, int address, int elementSize) {
            array = new AbstractExternal<T, BindingClass>[length];
            for (var i = 0; i<length; ++i) {
                array[i] = new AbstractExternal<T, BindingClass>(address + i * elementSize);
            }
            valuesArray = new Values(array);
        }

        public unsafe AbstractExternalArray(int length, Func<IntPtr> GetBaseAddress, int offset, int elementSize) {
            array = new AbstractExternal<T, BindingClass>[length];
            for (var i = 0; i < length; ++i) {
                array[i] = new AbstractExternal<T, BindingClass>(GetBaseAddress, offset + i * elementSize);
            }
            valuesArray = new Values(array);
        }

        public unsafe AbstractExternalArray(int length, AbstractExternal<IntPtr, BindingClass> parentObject, int offset, int elementSize) {
            array = new AbstractExternal<T, BindingClass>[length];
            for (var i = 0; i < length; ++i) {
                array[i] = new AbstractExternal<T, BindingClass>(parentObject, offset + i * elementSize);
            }
            valuesArray = new Values(array);
        }

        public AbstractExternal<T, BindingClass> this[int i] {
            get {
                return array[i];
            }
        }

        public Values ValuesArray {
            get {
                return valuesArray;
            }
        }

        public class Values {

            public AbstractExternal<T, BindingClass>[] array;

            public Values(AbstractExternal<T, BindingClass>[] array){
                this.array = array;
            }

            public T this[int i] {
                get {
                    return array[i].Value;
                }
            }
        }
    }
}
