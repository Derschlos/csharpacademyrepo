import camelot
import ctypes
import ghostscript

from ctypes.util import find_library

find_library("".join(("gsdll", str(ctypes.sizeof(ctypes.c_voidp) * 8), ".dll")))

tables = camelot.read_pdf('Grundwortschatz-Franzoesisch-Vokabeln.pdf',pages='1,2-end')
print(tables)
tables.export('foo.db', f='sqlite', compress=True)
